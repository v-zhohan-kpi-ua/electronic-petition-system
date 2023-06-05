export default function removeProps<
  T extends { [key: string]: unknown },
  K extends keyof T
>(object: T, keys: K[]): Omit<T, K> {
  return Object.entries(object).reduce((accumulator, [key, value]) => {
    if (!keys.includes(key as K)) {
      return { ...accumulator, [key]: value };
    }
    return accumulator;
  }, {} as Omit<T, K>);
}
