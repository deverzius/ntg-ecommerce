import { FileInput, Image } from "@mantine/core";
import { productLabels } from "@/shared/constants/product";
import type { ProductResponseDto } from "@/shared/types/dtos/product/response";
import { notifications } from "@mantine/notifications";
import { useUploadFileMutation } from "@/hooks/file/useUploadFileMutation";
import { useGetFileUrlQuery } from "@/hooks/file/useGetFileUrlQuery";
import { getImageUrlFromSignedUrl } from "@/shared/utils/getImageUrlFromSignedUrl";

interface ProductImagesInputProps {
  product: ProductResponseDto;
  onUploadSuccess: (fileName: string, filePath: string) => void;
}

export function ProductImagesInput({
  product,
  onUploadSuccess,
}: ProductImagesInputProps) {
  const { mutateAsync: uploadFile } = useUploadFileMutation();
  const { data: fileUrlResponse } = useGetFileUrlQuery({
    filePath: product.images[0]?.path,
  });

  function handleFileUpload(file: File | null) {
    if (!file) {
      return;
    }

    const fileName = `image-${crypto.randomUUID()}-${new Date().getTime()}`;

    uploadFile({ name: fileName, file })
      .then((result) => {
        notifications.show({
          color: "green",
          title: "Success",
          message: "File uploaded successfully.",
        });

        // TODO: Handle upload multiple files
        onUploadSuccess(fileName, result.path);
      })
      .catch(() => {
        notifications.show({
          color: "red",
          title: "Error",
          message: "Failed to upload file.",
        });
      });
  }

  return (
    <>
      <FileInput
        label={productLabels.files}
        placeholder="Click to upload file"
        accept="image/png,image/jpeg,image/jpg,image/gif,image/webp"
        onChange={(payload) => handleFileUpload(payload)}
      />

      {fileUrlResponse?.signedURL && (
        <Image
          src={getImageUrlFromSignedUrl(fileUrlResponse?.signedURL)}
          alt="product-img"
          w={100}
        />
      )}
    </>
  );
}
