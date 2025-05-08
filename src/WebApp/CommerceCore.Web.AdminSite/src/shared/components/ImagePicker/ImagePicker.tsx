import { useState } from "react";
import {
  Modal,
  Image,
  SimpleGrid,
  Button,
  Text,
  ScrollArea,
  Box,
  Flex,
  Divider,
} from "@mantine/core";
import { FontWeight } from "@/shared/types/enum";
import { useGetPublicFileUrlsQuery } from "@/hooks/file/useGetPublicFileUrlsQuery";
import { LoadingIndicator } from "../LoadingIndicator/LoadingIndicator";
import { ProductImagesInput } from "@/pages/products/components/ProductImagesInput";
import { notifications } from "@mantine/notifications";
import type { ProductImageResponseDto } from "@/shared/types/dtos/product/response";
import type { ProductImageRequestDto } from "@/shared/types/dtos/product/request";
import classes from "./ImagePicker.module.css";

interface ImagePickerProps {
  productId: string;
  defaultImages?: ProductImageResponseDto[];
  handleSubmit: (imagePaths: ProductImageRequestDto[]) => void;
}

export default function ImagePicker({
  productId,
  handleSubmit,
  defaultImages,
}: ImagePickerProps) {
  const [opened, setOpened] = useState(false);
  const [selectedUrls, setSelectedUrls] = useState<string[]>(
    defaultImages?.map((img) => img.publicUrl) || []
  );

  const {
    isLoading,
    data: images,
    refetch,
  } = useGetPublicFileUrlsQuery({
    limit: 50,
    offset: 0,
  });

  const handleUploadSuccess = () => {
    refetch();
  };

  const handleClickImage = (url: string) => {
    if (selectedUrls.includes(url)) {
      setSelectedUrls(selectedUrls.filter((img) => img !== url));
      return;
    }

    if (selectedUrls.length == 3) {
      notifications.show({
        color: "red",
        title: "Error",
        message: "You can't select more than 3 images",
      });
      return;
    }

    setSelectedUrls([...selectedUrls, url]);
  };

  return (
    <>
      <Text fw={FontWeight.Medium} mb="xs">
        Product Images
      </Text>
      <Button color="gray" variant="outline" onClick={() => setOpened(true)}>
        Open Gallery
      </Button>

      <Modal
        opened={opened}
        onClose={() => setOpened(false)}
        withCloseButton
        size="xl"
        title="Select an Image"
      >
        {isLoading ? (
          <LoadingIndicator />
        ) : (
          <Box>
            <ProductImagesInput
              productId={productId}
              onUploadSuccess={handleUploadSuccess}
            />
            <Flex direction="row" gap="lg" align="center">
              <Text fw={FontWeight.Medium}>Selected Images</Text>
              <SimpleGrid cols={3} flex={1}>
                {selectedUrls.map((url, index) => (
                  <Box pos="relative" className={classes.imageBtn}>
                    <Image
                      mah={100}
                      key={index}
                      src={url}
                      alt={`Image ${index}`}
                      onClick={() => handleClickImage(url)}
                    />
                    <Text pos="absolute" top={0} right={0} fz="lg">
                      &#10005;
                    </Text>
                  </Box>
                ))}
              </SimpleGrid>
              <Button
                w={150}
                onClick={() => {
                  const selectedRequestImages = selectedUrls.map((url) => {
                    const path = images?.find(
                      (img) => img.publicUrl === url
                    )?.path;
                    const name = path?.split("/")[1];

                    return {
                      name: name || "",
                      path: path || "",
                    };
                  });

                  handleSubmit(selectedRequestImages);
                  setOpened(false);
                }}
              >
                Done
              </Button>
            </Flex>

            <Divider color="gray.4" my={20} />

            <ScrollArea h={400}>
              <SimpleGrid cols={5}>
                {images?.map((img, index) => (
                  <Image
                    p={selectedUrls.includes(img.publicUrl) ? 2 : 0}
                    mah={200}
                    key={index}
                    src={img.publicUrl}
                    alt={`Image ${index}`}
                    onClick={() => handleClickImage(img.publicUrl)}
                    style={{
                      cursor: "pointer",
                      border: selectedUrls.includes(img.publicUrl)
                        ? "2px solid var(--mantine-color-blue-5)"
                        : "none",
                    }}
                  />
                ))}
              </SimpleGrid>
            </ScrollArea>
          </Box>
        )}
      </Modal>
    </>
  );
}
