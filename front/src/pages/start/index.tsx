import StartPetitionPageLayout from "@src/layouts/start/index";
import Head from "next/head";

export default function StartPetition() {
  return (
    <>
      <Head>
        <title key="title">
          Розпочати петицію. Уряд Королівства Дикої Природи та Лісів
        </title>
      </Head>
      <StartPetitionPageLayout />
    </>
  );
}
