import { FileInput } from "@mantine/core";
import { productLabels } from "@/shared/constants/product";
import { notifications } from "@mantine/notifications";
import { useUploadFileMutation } from "@/hooks/file/useUploadFileMutation";

interface ProductImagesInputProps {
  onUploadSuccess?: () => void;
  productId: string;
}

export function ProductImagesInput({
  productId,
  onUploadSuccess,
}: ProductImagesInputProps) {
  const { mutateAsync: uploadFile, isPending: isUploadingFile } =
    useUploadFileMutation();

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

        onUploadSuccess && onUploadSuccess();
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
        description={
          isUploadingFile ? "Uploading..." : "Click to upload product image"
        }
        disabled={isUploadingFile}
        placeholder={"Click to upload file"}
        accept="image/png,image/jpeg,image/jpg,image/gif,image/webp"
        onChange={(payload) => handleFileUpload(payload)}
      />
    </>
  );
}
