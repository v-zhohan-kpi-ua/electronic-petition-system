import fetcher from "@src/api/fetcher";
import { AxiosError } from "axios";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";

export default function useAuthAdmin({
  redirectPath,
}: {
  redirectPath: string;
}) {
  const router = useRouter();
  const [isAuthValid, setIsAuthValid] = useState(() => false);

  useEffect(() => {
    const authValidCheck = async () => {
      try {
        await fetcher.get("/admin/ping");
        setIsAuthValid(true);
      } catch (e) {
        if (e instanceof AxiosError) {
          if (e.response?.status === 401 || e.response?.status === 403) {
            router.push(redirectPath);
          }
        }
      }
    };

    authValidCheck();
  }, [redirectPath, router]);

  return isAuthValid;
}
