type MapSelectOption<T, K extends keyof T, V extends keyof T> = {
  label: T[K];
  value: T[V];
};

export function mapSelectOptions<T, K extends keyof T, V extends keyof T>(
  data: Array<T>,
  labelField: K,
  valueField: V
): MapSelectOption<T, K, V>[] {
  return data.map((item) => ({
    label: item[labelField],
    value: item[valueField],
  }));
}
