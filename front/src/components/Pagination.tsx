import { RequireAtLeastOne } from "@src/utils/types";
import { HiOutlineChevronLeft, HiOutlineChevronRight } from "react-icons/hi";
import ReactPaginate, { ReactPaginateProps } from "react-paginate";

type PaginationProps = RequireAtLeastOne<
  {
    setCurrentPage: (page: number) => unknown;
    currentPage: number;
    pageCount: number;
    itemCount: number;
    itemsPerPage?: number;
  } & ReactPaginateProps,
  "pageCount" | "itemCount"
>;

export default function Pagination({
  setCurrentPage,
  currentPage = 0,
  pageCount,
  itemsPerPage = 10,
  itemCount,
  ...rest
}: PaginationProps) {
  const calculatedPageCount = pageCount
    ? pageCount
    : itemCount && Math.ceil(itemCount / itemsPerPage);

  if (calculatedPageCount === undefined) {
    throw new Error("Pagination: pageCount or itemCount prop is required");
  }

  return (
    <ReactPaginate
      {...rest}
      pageCount={calculatedPageCount}
      initialPage={currentPage}
      onPageChange={({ selected }) => setCurrentPage(selected)}
      containerClassName="flex max-sm:justify-between sm:inline-flex flex-row -space-x-px"
      pageClassName="hidden sm:inline-flex items-center text-sm font-semibold text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-100 focus:z-20"
      pageLinkClassName="px-4 py-2"
      activeClassName="ring-0 z-10 bg-primary-600 hover:bg-primary-700 text-white"
      breakClassName="hidden sm:inline-flex items-center text-sm font-semibold text-gray-700 ring-1 ring-inset ring-gray-300"
      breakLinkClassName="px-4 py-2"
      previousClassName="inline-flex text-md sm:text-sm text-gray-400 items-center max-sm:rounded-md sm:rounded-l-md ring-1 ring-inset ring-gray-300 hover:bg-gray-100"
      previousLinkClassName="px-6 sm:px-2.5 py-2"
      previousLabel={<HiOutlineChevronLeft />}
      nextClassName="inline-flex text-sm text-gray-400 items-center max-sm:rounded-md sm:rounded-r-md ring-1 ring-inset ring-gray-300 hover:bg-gray-100"
      nextLinkClassName="px-6 sm:px-2.5 py-2"
      disabledClassName="bg-gray-200 hover:bg-gray-200 text-gray-400"
      nextLabel={<HiOutlineChevronRight />}
      ariaLabelBuilder={(page, selectedPage) => {
        if (selectedPage) {
          return `Ви зараз перебуваєте на сторінці ${page}`;
        }

        return `Сторінка ${page}`;
      }}
      previousAriaLabel="Попередня сторінка"
      nextAriaLabel="Наступна сторінка"
    />
  );
}
