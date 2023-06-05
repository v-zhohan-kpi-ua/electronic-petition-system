import AdminMenu from "@src/components/AdminMenu";
import Link from "next/link";

type AdminIndexPageLayoutProps = {
  petitionsStats: {
    waitForAnswer: number;
    waitForModeration: number;
  };
};

export default function AdminIndexPageLayout({
  petitionsStats,
}: AdminIndexPageLayoutProps) {
  return (
    <div>
      <AdminMenu />
      <div className="flex flex-col sm:flex-row gap-6 sm:gap-12 text-primary-700">
        <Link href="/admin/review">
          <div className="flex flex-col hover:text-primary-500">
            <div className="text-4xl font-bold">
              {petitionsStats.waitForAnswer}
            </div>
            <div className="text-lg font-medium">
              петицій очікують відповідь
            </div>
          </div>
        </Link>
        <Link href="/admin/mod">
          <div className="flex flex-col  hover:text-primary-500">
            <div className="text-4xl font-bold">
              {petitionsStats.waitForModeration}
            </div>
            <div className="text-lg font-medium">
              петицій очікують модерації
            </div>
          </div>
        </Link>
      </div>
    </div>
  );
}
