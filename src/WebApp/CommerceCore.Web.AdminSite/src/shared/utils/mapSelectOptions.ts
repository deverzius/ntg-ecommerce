type MapSelectOption<T, K extends keyof T, V extends keyof T> = {
    label: T[K];
    value: T[V];
};

type FilterFn<T> = (item: T) => boolean;

const defaultFilterFn = () => true;

export function mapSelectOptions<T, K extends keyof T, V extends keyof T>(
    data: Array<T>,
    labelField: K,
    valueField: V,
    filterFn?: FilterFn<T>
): MapSelectOption<T, K, V>[] {
    return data.filter(filterFn || defaultFilterFn).map((item) => ({
        label: item[labelField],
        value: item[valueField],
    }));
}
