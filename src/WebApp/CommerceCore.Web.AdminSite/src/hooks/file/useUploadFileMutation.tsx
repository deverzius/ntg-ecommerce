import {uploadFile} from "@/services/fileServices";
import {useMutation} from "@tanstack/react-query";

type MutationFn = {
    name: string;
    file: File;
};

export function useUploadFileMutation() {
    return useMutation({
        mutationFn: ({name, file}: MutationFn) => uploadFile(name, file),
    });
}
