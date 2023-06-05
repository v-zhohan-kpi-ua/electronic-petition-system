import PetitionsPageLayout from "@src/layouts/petitions/index";
import Head from "next/head";

export default function Petition() {
  return (
    <>
      <Head>
        <title key="title">
          Знайти петицію. Уряд Королівства Дикої Природи та Лісів
        </title>
      </Head>
      <PetitionsPageLayout />
    </>
  );
}
