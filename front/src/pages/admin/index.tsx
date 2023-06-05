import { useGetPetitions } from "@src/api/hooks";
import useAuthAdmin from "@src/hooks/useAuthAdmin";
import LoadingPage from "@src/layouts/LoadingPage";
import AdminIndexPageLayout from "@src/layouts/admin/index";
import Head from "next/head";

export default function AdminPage() {
  const isAuthValid = useAuthAdmin({ redirectPath: "/admin/login" });

  const { data: waitingForAnswerPetitions, isSuccess: isSuccessAnswer } =
    useGetPetitions({
      status: ["WaitingForAnswer"],
      perPage: 0,
    });

  const {
    data: waitingForModerationPetitions,
    isSuccess: isSuccessModeration,
  } = useGetPetitions({
    status: ["Created"],
    perPage: 0,
  });

  return (
    <>
      {!isAuthValid && <LoadingPage />}
      {isAuthValid && isSuccessAnswer && isSuccessModeration && (
        <>
          <Head>
            <title key="title">
              Адміністрування петицій. Уряд Королівства Дикої Природи та Лісів
            </title>
          </Head>
          <AdminIndexPageLayout
            petitionsStats={{
              waitForAnswer: waitingForAnswerPetitions?.totalItems ?? 0,
              waitForModeration: waitingForModerationPetitions?.totalItems ?? 0,
            }}
          />
        </>
      )}
    </>
  );
}
