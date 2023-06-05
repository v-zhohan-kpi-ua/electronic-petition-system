import Footer from "@src/components/Footer";
import Header from "@src/components/Header";
import "@src/styles/globals.css";
import type { AppProps } from "next/app";
import { Roboto } from "next/font/google";
import Head from "next/head";
import { useRouter } from "next/router";
import { useState } from "react";
import { Hydrate, QueryClient, QueryClientProvider } from "react-query";

const roboto = Roboto({
  subsets: ["cyrillic"],
  weight: ["300", "400", "500", "700", "900"],
});

export default function App({ Component, pageProps }: AppProps) {
  const [queryClient] = useState(() => new QueryClient());

  return (
    <>
      <Head>
        <link rel="preconnect" href="https://fonts.googleapis.com" />
        <link
          rel="preconnect"
          href="https://fonts.gstatic.com"
          crossOrigin=""
        />
        <title key="title">
          Петиції: Уряд Королівства Дикої Природи та Лісів
        </title>
      </Head>
      <style jsx global>
        {`
          :root {
            --font-roboto: ${roboto.style.fontFamily};
          }
        `}
      </style>
      <QueryClientProvider client={queryClient}>
        <Hydrate state={pageProps.dehydratedState}>
          <div className="flex flex-col h-screen">
            <Header />
            <main className="flex-1">
              <div className="max-w-screen-md mx-auto py-5 h-full">
                <Component {...pageProps} />
              </div>
            </main>
            <Footer />
          </div>
        </Hydrate>
      </QueryClientProvider>
    </>
  );
}
