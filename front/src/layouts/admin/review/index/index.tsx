import { useGetPetitionsInfinity } from "@src/api/hooks";
import AdminMenu from "@src/components/AdminMenu";
import Link from "next/link";
import React from "react";
import { ImSpinner2 } from "react-icons/im";
import InfiniteScroll from "react-infinite-scroll-component";

export default function AdminReviewIndexPageLayout() {
  const perPage = 10;

  const { data, hasNextPage, fetchNextPage } = useGetPetitionsInfinity({
    order: "oldStatus",
    status: ["WaitingForAnswer"],
    page: 1,
    perPage: perPage,
  });

  return (
    <>
      <AdminMenu />
      <h3 className="text-black font-bold text-2xl mb-4">Очікують відповідь</h3>
      <div className="mt-5">
        <InfiniteScroll
          className="space-y-4"
          style={{
            overflow: "hidden",
          }}
          dataLength={data?.pages.length ?? 0 * perPage}
          next={fetchNextPage}
          hasMore={hasNextPage ?? false}
          scrollThreshold={0.95}
          loader={
            <ImSpinner2 className="animate-spin mx-auto text-2xl text-gray-500" />
          }
          endMessage={
            <div>
              <hr />
              <b className="text-lg text-black font-semibold">
                Це кінець списку
              </b>
            </div>
          }
        >
          {data?.pages &&
            data.pages.map((group) => (
              <React.Fragment key={group.currentPage}>
                {group.items.map((p) => (
                  <Petition key={p.id} petition={p} />
                ))}
              </React.Fragment>
            ))}
        </InfiniteScroll>
      </div>
    </>
  );
}

function Petition({ petition }: { petition: Petition }) {
  return (
    <div>
      <Link href={`/admin/review/${petition.id}`}>
        <div className="text-xl text-primary-700 hover:text-primary-500 font-semibold">
          {`${petition.title} (№${petition.id})`}
        </div>
      </Link>
    </div>
  );
}
