import { IconType } from "react-icons";
import { ImSpinner2 } from "react-icons/im";

import clsxTwMerge from "@src/utils/clsxTwMerge";
import { forwardRef } from "react";

const ButtonVariant = ["primary", "outline", "ghost"] as const;
const ButtonSize = ["sm", "base"] as const;

type ButtonProps = {
  isLoading?: boolean;
  isDarkBg?: boolean;
  variant?: typeof ButtonVariant[number];
  size?: typeof ButtonSize[number];
  leftIcon?: IconType;
  rightIcon?: IconType;
  leftIconClassName?: string;
  rightIconClassName?: string;
  isError?: boolean;
} & React.ComponentPropsWithRef<"button">;

const Button = forwardRef<HTMLButtonElement, ButtonProps>(function Button(
  {
    children,
    className,
    disabled,
    isLoading,
    variant = "primary",
    size = "base",
    isDarkBg = false,
    leftIcon: LeftIcon,
    rightIcon: RightIcon,
    leftIconClassName,
    rightIconClassName,
    isError,
    ...rest
  },
  ref
) {
  const isDisabled = disabled || isLoading;

  return (
    <button
      ref={ref}
      type="button"
      disabled={isDisabled}
      className={clsxTwMerge(
        "inline-flex items-center justify-center rounded-lg font-medium",
        "focus:outline-none focus-visible:ring-2 focus-visible:ring-primary-500",
        "shadow-sm",
        "transition-colors duration-75",
        [
          size === "base" && ["px-3 py-1.5", "text-sm md:text-base"],
          size === "sm" && ["px-2 py-1", "text-xs md:text-sm"],
        ],
        [
          variant === "primary" && [
            "bg-primary-500 text-white",
            "border border-primary-600",
            "hover:bg-primary-600 hover:text-white",
            "active:bg-primary-700",
            "disabled:bg-primary-700",
          ],
          variant === "outline" && [
            "text-primary-500",
            "border border-primary-500",
            "hover:bg-primary-50 active:bg-primary-100 disabled:bg-primary-100",
            isDarkBg &&
              "hover:bg-gray-900 active:bg-gray-800 disabled:bg-gray-800",
          ],
          variant === "ghost" && [
            "text-primary-500",
            "shadow-none",
            "hover:bg-primary-50 active:bg-primary-100 disabled:bg-primary-100",
            isDarkBg &&
              "hover:bg-gray-900 active:bg-gray-800 disabled:bg-gray-800",
          ],
        ],
        isError &&
          "text-red-500 focus:ring-red-500 border-red-500 focus:border-red-500",
        isLoading &&
          "relative text-transparent transition-none hover:text-transparent",
        className
      )}
      {...rest}
    >
      {isLoading && (
        <div
          className={clsxTwMerge(
            "absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2",
            {
              "text-white": ["primary"].includes(variant),
              "text-primary-500": ["outline", "ghost"].includes(variant),
            }
          )}
        >
          <ImSpinner2 className="animate-spin" />
        </div>
      )}
      {LeftIcon && (
        <div
          className={clsxTwMerge([
            size === "base" && "mr-1",
            size === "sm" && "mr-1.5",
          ])}
        >
          <LeftIcon
            className={clsxTwMerge(
              [
                size === "base" && "md:text-md text-md",
                size === "sm" && "md:text-md text-sm",
              ],
              leftIconClassName
            )}
          />
        </div>
      )}
      {children}
      {RightIcon && (
        <div
          className={clsxTwMerge([
            size === "base" && "ml-1",
            size === "sm" && "ml-1.5",
          ])}
        >
          <RightIcon
            className={clsxTwMerge(
              [
                size === "base" && "text-md md:text-md",
                size === "sm" && "md:text-md text-sm",
              ],
              rightIconClassName
            )}
          />
        </div>
      )}
    </button>
  );
});

export default Button;
