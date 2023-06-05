import fetcher from "@src/api/fetcher";
import { AxiosError } from "axios";
import { useRouter } from "next/router";

export default function AdminMenu() {
  const router = useRouter();

  const logout = async () => {
    try {
      await fetcher.get("/auth/logout");
      router.push("/admin/login");
    } catch (e) {
      if (e instanceof AxiosError) {
        router.push("/admin/login");
      }
    }
  };

  return (
    <div className="flex flex-row justify-end mb-3">
      <button onClick={logout}>Вихід →</button>
    </div>
  );
}
