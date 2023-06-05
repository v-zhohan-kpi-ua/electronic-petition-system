import Head from "next/head";

export default function NotFoundPage() {
  return (
    <>
      <Head>
        <title key="title">
          Сторінку не знайдено. Петиції: Уряд Королівства Дикої Природи та Лісів
        </title>
      </Head>
      <div className="flex justify-center items-center h-full text-black text-lg">
        <div>Сторінку не знайдено | 404</div>
      </div>
    </>
  );
}
