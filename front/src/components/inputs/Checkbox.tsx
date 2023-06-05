import clsxTwMerge from "@src/utils/clsxTwMerge";
import { RegisterOptions, useFormContext } from "react-hook-form";

export type CheckboxProps = {
  label: string;
  id: string;
  disabled?: boolean;
  validation?: RegisterOptions;
} & React.ComponentPropsWithoutRef<"input">;

export default function Checkbox({
  label,
  id,
  disabled = false,
  validation,
  ...rest
}: CheckboxProps) {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <div>
      <div className="flex items-center">
        <input
          {...register(id, validation)}
          {...rest}
          id={id}
          aria-describedby={id}
          disabled={disabled}
          type="checkbox"
          className={clsxTwMerge(
            "w-5 h-5 text-primary-600 bg-gray-100 border-gray-300 rounded focus:ring-primary-500",
            errors[id] && "focus:ring-red-500",
            disabled && "focus:ring-0 focus:border-gray-300"
          )}
        />
        <label
          htmlFor={id}
          className={clsxTwMerge(
            "ml-2 text-sm font-normal text-gray-700",
            errors[id] && "text-red-500",
            disabled && "text-gray-400"
          )}
        >
          {label}
        </label>
      </div>
      {errors[id] && (
        <div className="mt-1 text-sm text-red-500">
          {errors[id]?.message as unknown as string}
        </div>
      )}
    </div>
  );
}
