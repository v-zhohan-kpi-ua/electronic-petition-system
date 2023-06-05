import clsxTwMerge from "@src/utils/clsxTwMerge";
import { forwardRef } from "react";
import { IconType } from "react-icons";
import { HiExclamationCircle } from "react-icons/hi";

export type BaseInputProps = {
  id: string;
  label: string;
  isError?: boolean;
  errorMessage?: string;
  hideLabel?: boolean;
  icon?: IconType;
  iconClassName?: string;
} & React.ComponentPropsWithoutRef<"input">;

const BaseInput = forwardRef<HTMLInputElement, BaseInputProps>(
  function BaseInput(
    {
      id,
      label,
      placeholder = "",
      type = "text",
      isError,
      errorMessage,
      disabled = false,
      hideLabel = false,
      icon: Icon,
      iconClassName,
      ...rest
    },
    ref
  ) {
    return (
      <div>
        <label
          htmlFor={id}
          className={clsxTwMerge(
            "block text-sm font-normal text-gray-700",
            hideLabel && "sr-only"
          )}
        >
          {label}
        </label>
        <div className={clsxTwMerge("relative", !hideLabel && "mt-1")}>
          <input
            ref={ref}
            type={type}
            name={id}
            id={id}
            disabled={disabled}
            className={clsxTwMerge(
              "block w-full rounded-lg shadow-sm",
              "focus:ring-primary-500 border-gray-300 focus:border-primary-500",
              disabled &&
                "bg-gray-100 focus:ring-0 border-gray-300 focus:border-gray-300",
              isError &&
                "focus:ring-red-500 border-red-500 focus:border-red-500",
              isError && "pr-10",
              Icon && "pl-10"
            )}
            placeholder={placeholder}
            aria-describedby={id}
            {...rest}
          />

          {Icon && (
            <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none text-xl">
              <Icon className={clsxTwMerge("text-gray-400", iconClassName)} />
            </div>
          )}
          <div className="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none text-xl">
            {isError && <HiExclamationCircle className="text-red-500" />}
          </div>
        </div>
        <div className="mt-1 text-sm text-red-500 whitespace-pre">
          {errorMessage}
        </div>
      </div>
    );
  }
);

export default BaseInput;
