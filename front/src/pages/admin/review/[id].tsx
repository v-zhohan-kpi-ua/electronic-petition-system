import { useGetPetition } from "@src/api/hooks";
import useAuthAdmin from "@src/hooks/useAuthAdmin";
import LoadingPage from "@src/layouts/LoadingPage";
import AdminReviewByIdPageLayout from "@src/layouts/admin/review/id";
import Head from "next/head";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";

export default function AdminReviewByIdPage() {
  const isAuthValid = useAuthAdmin({ redirectPath: "/admin/login" });

  const [needsAnswer, setNeedsAnswer] = useState<boolean | undefined>();

  const router = useRouter();
  const { id } = router.query;

  const { data, isSuccess, isLoading } = useGetPetition({
    id: Number(id),
  });

  useEffect(() => {
    if (isSuccess) {
      setNeedsAnswer(data.status === "WaitingForAnswer");
    }
  }, [isSuccess, data]);

  useEffect(() => {
    if (needsAnswer === false) {
      router.push("/admin/review");
    }
  }, [needsAnswer, router]);

  return (
    <>
      {(!isAuthValid || isLoading || !needsAnswer) && <LoadingPage />}
      {isSuccess && needsAnswer && (
        <>
          <Head>
            <title key="title">
              {`Відповісти на петицію №${data.id}. Уряд Королівства Дикої Природи та Лісів`}
            </title>
          </Head>
          <AdminReviewByIdPageLayout petition={data} />
        </>
      )}
    </>
  );
}
