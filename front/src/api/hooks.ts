import fetcher from "@src/api/fetcher";
import { useInfiniteQuery, useQuery } from "react-query";
import qs from "qs";
import { getPetition, getPetitions } from "@src/api";

export const useGetPetitions = ({
  search,
  status,
  order,
  page,
  perPage,
  enabled = true,
  keepPreviousData = false,
}: Parameters<typeof getPetitions>[0] & {
  enabled?: boolean;
  keepPreviousData?: boolean;
}) => {
  return useQuery({
    queryKey: ["petitions", { search, status, order, page, perPage }],
    queryFn: () => getPetitions({ search, status, order, page, perPage }),
    enabled,
    keepPreviousData: keepPreviousData,
  });
};

export const useGetPetitionsInfinity = ({
  search,
  status,
  order,
  page,
  perPage,
}: Parameters<typeof getPetitions>[0]) => {
  return useInfiniteQuery({
    queryKey: ["petitions/infinity", { search, status, order, perPage }],
    queryFn: ({ pageParam = page }) =>
      getPetitions({ search, status, order, page: pageParam, perPage }),
    getNextPageParam: (page) =>
      page.hasNextPage ? page.currentPage + 1 : undefined,
  });
};

export const useGetPetition = ({ id }: { id: number }) => {
  return useQuery({
    queryKey: ["petition", { id }],
    queryFn: () => getPetition({ id }),
  });
};
