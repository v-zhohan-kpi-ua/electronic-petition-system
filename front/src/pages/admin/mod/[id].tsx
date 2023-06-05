import { useGetPetition } from "@src/api/hooks";
import useAuthAdmin from "@src/hooks/useAuthAdmin";
import LoadingPage from "@src/layouts/LoadingPage";
import AdminModByIdPageLayout from "@src/layouts/admin/mod/id";
import Head from "next/head";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";

export default function AdminModByIdPage() {
  const isAuthValid = useAuthAdmin({ redirectPath: "/admin/login" });

  const [needsMod, setNeedsMod] = useState<boolean | undefined>();

  const router = useRouter();
  const { id } = router.query;

  const { data, isSuccess, isLoading } = useGetPetition({
    id: Number(id),
  });

  useEffect(() => {
    if (isSuccess) {
      setNeedsMod(data.status === "Created");
    }
  }, [isSuccess, data]);

  useEffect(() => {
    if (needsMod === false) {
      router.push("/admin/mod");
    }
  }, [needsMod, router]);

  return (
    <>
      {(!isAuthValid || isLoading || !needsMod) && <LoadingPage />}
      {isSuccess && needsMod && (
        <>
          <Head>
            <title key="title">
              {`Промодерувати петицію №${data.id}. Уряд Королівства Дикої Природи та Лісів`}
            </title>
          </Head>
          <AdminModByIdPageLayout petition={data} />
        </>
      )}
    </>
  );
}
