import Button from "@src/components/buttons/Button";
import { useEffect, useState } from "react";
import BaseInput from "@src/components/inputs/BaseInput";
import { HiOutlineSearch } from "react-icons/hi";
import { useRouter } from "next/router";
import Pagination from "@src/components/Pagination";
import removeProps from "@src/utils/removeProps";
import { useGetPetitions } from "@src/api/hooks";
import { declensionOfDay, declensionOfSign } from "@src/utils/lang";
import { differenceInDays } from "date-fns";
import useDebounce from "@src/hooks/useDebounce";
import Link from "next/link";

enum PetitionState {
  ANSWERED = "answered",
  OPEN = "open",
  CLOSED = "closed",
}

const petitionStateTransformToPetitionStatus = (
  state: PetitionState
): PetitionStatus[] => {
  switch (state) {
    case PetitionState.CLOSED:
      return ["NotEnoughSigns", "Declined"];
    case PetitionState.ANSWERED:
      return ["Answered"];
    case PetitionState.OPEN:
      return ["Signing", "WaitingForAnswer"];
  }
};

export default function PetitionsPageLayout() {
  const router = useRouter();
  const { isReady } = router;
  const { state: routerQueryState, q: routerQuerySearch } = router.query;

  const [petitionQueryState, setPetitionQueryState] = useState<PetitionState>(
    PetitionState.OPEN
  );
  const [petitionQuerySearch, setPetitionQuerySearch] = useState("");
  const debouncedQuerySearch = useDebounce(petitionQuerySearch, 1000);

  const [petitionPage, setPetitionPage] = useState(0);

  const { data } = useGetPetitions({
    search:
      debouncedQuerySearch.trim() === ""
        ? undefined
        : petitionQuerySearch.trim(),
    order: "oldStatus",
    status: petitionStateTransformToPetitionStatus(petitionQueryState),
    page: petitionPage + 1,
    perPage: 10,
  });

  useEffect(() => {
    if (isReady) {
      if (
        Object.values(PetitionState).includes(routerQueryState as PetitionState)
      ) {
        setPetitionQueryState(routerQueryState as PetitionState);
      }

      if (routerQuerySearch) {
        setPetitionQuerySearch(routerQuerySearch as string);
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isReady]);

  useEffect(() => {
    window.scroll({ top: 0, behavior: "smooth" });
  }, [petitionPage]);

  const onPetitionStateChange = (state: PetitionState) => {
    setPetitionQueryState(state);
    router.push({ query: { ...router.query, state: state } }, undefined, {
      shallow: true,
    });
  };

  const onPetitionQueryChange = (query: string) => {
    setPetitionQuerySearch(query);

    const routerQuery = {
      ...router.query,
      q: query.trim(),
    };

    router.push(
      {
        query:
          query.trim() === "" ? removeProps(routerQuery, ["q"]) : routerQuery,
      },
      undefined,
      {
        shallow: true,
      }
    );
  };

  return (
    <>
      <h3 className="text-black font-bold text-2xl mb-4 text-center">
        Пошук петицій
      </h3>
      <div className="flex flex-col md:flex-row justify-center">
        <Button
          variant={
            petitionQueryState === PetitionState.ANSWERED
              ? "primary"
              : "outline"
          }
          disabled={petitionQueryState === PetitionState.ANSWERED}
          onClick={() => onPetitionStateChange(PetitionState.ANSWERED)}
          className="max-md:rounded-b-none md:rounded-r-none"
        >
          Отримали відповідь
        </Button>
        <Button
          variant={
            petitionQueryState === PetitionState.OPEN ? "primary" : "outline"
          }
          disabled={petitionQueryState === PetitionState.OPEN}
          onClick={() => onPetitionStateChange(PetitionState.OPEN)}
          className="rounded-none"
        >
          Відкриті для підпису
        </Button>
        <Button
          variant={
            petitionQueryState === PetitionState.CLOSED ? "primary" : "outline"
          }
          disabled={petitionQueryState === PetitionState.CLOSED}
          onClick={() => onPetitionStateChange(PetitionState.CLOSED)}
          className="max-md:rounded-t-none md:rounded-l-none"
        >
          Завершені
        </Button>
      </div>
      <div className="mt-6 max-w-2xl mx-auto">
        <BaseInput
          id="search"
          label="Пошук петицій"
          hideLabel
          icon={HiOutlineSearch}
          value={petitionQuerySearch}
          onChange={(e) => onPetitionQueryChange(e.target.value)}
        />
      </div>
      <div>
        <div className="mt-5 space-y-4">
          {data && data.totalItems === 0 && (
            <div className="text-center text-lg text-black">
              Нічого не знайдено
            </div>
          )}

          {data &&
            data.totalItems > 0 &&
            data.items.map((p) => <Petition key={p.id} petition={p} />)}
        </div>
        {data && data.totalPages >= 2 && (
          <div className="mt-2 block sm:flex justify-center">
            <Pagination
              currentPage={petitionPage}
              setCurrentPage={(page) => setPetitionPage(page)}
              itemCount={data.totalItems}
            />
          </div>
        )}
      </div>
    </>
  );
}

function Petition({ petition }: { petition: Petition }) {
  const daysLeft = differenceInDays(
    new Date(petition.statusDeadline ?? ""),
    new Date()
  );

  return (
    <div>
      <Link href={`/petitions/${petition.id}`}>
        <div className="text-xl text-primary-700 hover:text-primary-500 font-semibold">
          {petition.title}
        </div>
      </Link>
      <div className="text-md text-gray-500">
        {`${petition.signsCount.toLocaleString()} ${declensionOfSign(
          petition.signsCount
        )}`}
      </div>
      {daysLeft > 0 && (
        <div className="text-md text-gray-500">
          Залишилося <span className="font-semibold text-lg">{daysLeft}</span>{" "}
          {declensionOfDay(daysLeft)}
        </div>
      )}
      {petition.status === "WaitingForAnswer" && (
        <div className="text-md text-gray-500">Очікує відповідь</div>
      )}
    </div>
  );
}
