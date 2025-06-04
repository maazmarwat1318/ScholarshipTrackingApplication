export function convertWithSafeFallBackToValue(
  value: any,
  fallbackValue: number = 1,
) {
  const number = Number(value);
  return isNaN(number) ? fallbackValue : number;
}
