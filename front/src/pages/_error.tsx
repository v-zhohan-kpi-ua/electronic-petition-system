import { NextPage, NextPageContext } from "next";
import Head from "next/head";

interface Props {
  statusCode?: number;
}

const Error: NextPage<Props> = ({ statusCode }) => {
  return (
    <>
      <Head>
        <title key="title">
          Щось пішло не так. Петиції: Уряд Королівства Дикої Природи та Лісів
        </title>
      </Head>
      <div className="flex justify-center items-center h-full text-black text-lg">
        <div>{`Щось пішло не так${statusCode ? ` | ${statusCode}` : ""}`}</div>
      </div>
    </>
  );
};

Error.getInitialProps = ({ res, err }: NextPageContext) => {
  const statusCode = res ? res.statusCode : err ? err.statusCode : 404;
  return { statusCode };
};

export default Error;
