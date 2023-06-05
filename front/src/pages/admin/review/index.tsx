import useAuthAdmin from "@src/hooks/useAuthAdmin";
import LoadingPage from "@src/layouts/LoadingPage";
import AdminReviewIndexPageLayout from "@src/layouts/admin/review/index";
import Head from "next/head";

export default function AdminReviewIndexPage() {
  const isAuthValid = useAuthAdmin({ redirectPath: "/admin/login" });

  return (
    <>
      {isAuthValid ? (
        <>
          <Head>
            <title key="title">
              Очікують відповідь. Уряд Королівства Дикої Природи та Лісів
            </title>
          </Head>
          <AdminReviewIndexPageLayout />
        </>
      ) : (
        <LoadingPage />
      )}
    </>
  );
}
