import clsx, { ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export default function clsxTwMerge(...classNames: ClassValue[]) {
  return twMerge(clsx(...classNames));
}
