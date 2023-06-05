import fetcher from "@src/api/fetcher";
import { stringify } from "qs";

export const getPetition = async ({ id }: { id: number }) => {
  const response = await fetcher.get<Petition>(`/petitions/${id}`);

  return response.data;
};

export const getPetitions = async ({
  search,
  status = [],
  order = "id",
  page = 1,
  perPage = 10,
}: {
  search?: string;
  status?: PetitionStatus[];
  order?: "popularity" | "id" | "oldStatus";
  page?: number;
  perPage?: number;
}) => {
  const response = await fetcher.get<PaginationResponse<Petition>>(
    "/petitions",
    {
      params: {
        search,
        status,
        order,
        page,
        pageSize: perPage,
      },
      paramsSerializer: (params) =>
        stringify(params, { arrayFormat: "repeat" }),
    }
  );

  return response.data;
};
