import { RegisterOptions, useFormContext } from "react-hook-form";
import BaseInput, { BaseInputProps } from "@src/components/inputs/BaseInput";

export type InputProps = {
  validation?: RegisterOptions;
} & BaseInputProps;

export default function Input({ id, validation, ...rest }: InputProps) {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <BaseInput
      {...register(id, validation)}
      id={id}
      isError={!!errors[id]}
      errorMessage={errors[id]?.message as string}
      {...rest}
    />
  );
}
