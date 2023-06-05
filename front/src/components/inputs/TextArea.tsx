import clsxTwMerge from "@src/utils/clsxTwMerge";
import { RegisterOptions, useFormContext } from "react-hook-form";

export type TextAreaProps = {
  label: string;
  id: string;
  placeholder?: string;
  disabled?: boolean;
  validation?: RegisterOptions;
} & React.ComponentPropsWithoutRef<"textarea">;

export default function TextArea({
  label,
  placeholder = "",
  id,
  disabled = false,
  validation,
  className,
  maxLength = 120,
  ...rest
}: TextAreaProps) {
  const {
    register,
    watch,
    formState: { errors },
  } = useFormContext();

  const watchInput: string = watch(id) || "";
  const isInfinityMaxLength = !Number.isFinite(maxLength);

  return (
    <div>
      <label htmlFor={id} className="block text-sm font-normal text-gray-700">
        {label}
      </label>
      <div className="mt-1">
        <textarea
          {...register(id, validation)}
          {...rest}
          name={id}
          id={id}
          disabled={disabled}
          maxLength={maxLength}
          className={clsxTwMerge(
            "block w-full rounded-lg shadow-sm",
            "placeholder:text-sm",
            "focus:ring-primary-500 border-gray-300 focus:border-primary-500",
            disabled &&
              "bg-gray-100 focus:ring-0 border-gray-300 focus:border-gray-300",
            errors[id] &&
              "focus:ring-red-500 border-red-500 focus:border-red-500"
          )}
          placeholder={placeholder}
          aria-describedby={id}
        />
      </div>
      <div className="mt-1 text-sm text-red-500">
        {errors[id]?.message as unknown as string}
      </div>
      {!isInfinityMaxLength && (
        <div className="flex flex-col">
          <div className="place-self-end text-gray-700 text-sm">{`${watchInput.length}/${maxLength}`}</div>
        </div>
      )}
    </div>
  );
}
