import useAuthAdmin from "@src/hooks/useAuthAdmin";
import LoadingPage from "@src/layouts/LoadingPage";
import AdminModIndexPageLayout from "@src/layouts/admin/mod/index";
import Head from "next/head";

export default function AdminModIndexPage() {
  const isAuthValid = useAuthAdmin({ redirectPath: "/admin/login" });

  return (
    <>
      {isAuthValid ? (
        <>
          <Head>
            <title key="title">
              Очікують модерації. Уряд Королівства Дикої Природи та Лісів
            </title>
          </Head>
          <AdminModIndexPageLayout />
        </>
      ) : (
        <LoadingPage />
      )}
    </>
  );
}
