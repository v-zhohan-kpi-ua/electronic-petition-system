import Button from "@src/components/buttons/Button";
import { StartFormData } from "@src/layouts/start/index/StartForm";
import Link from "next/link";

type StartFormSuccessProps = {
  formData: StartFormData & { id: number };
};

export default function StartFormSuccess({ formData }: StartFormSuccessProps) {
  return (
    <div className="mt-5 space-y-4">
      <div className="text-lg text-black">
        Петицію{" "}
        <span className="italic hover:underline font-semibold">
          <Link
            href={`/petitions/${formData.id}`}
          >{`${formData.title} (№${formData.id})`}</Link>
        </span>{" "}
        успішно створено. Очікуйте рішення модерації
      </div>
      <div>
        <Link href="/">
          <Button variant="outline">Повернутися на головну сторінку</Button>
        </Link>
      </div>
    </div>
  );
}
