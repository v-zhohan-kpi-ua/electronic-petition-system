interface Petition {
  id: number;
  title: string;
  body: string;
  status: PetitionStatus;
  statusDeadline?: string;
  creator: Creator
  signsCount: number;
  signsRequiredToGetAnswer: number;
  answer?: string;
  moderationResult?: string;
  createdAt: string;
}

type PetitionStatus = "Created" | "Declined" | "Signing" | "NotEnoughSigns" | "WaitingForAnswer" | "Answered" 

interface Creator {
  firstName: string;
  lastName: string;
  email?: string;
}

interface PaginationResponse<T> {
  items: T[];
  totalItems: number;
  currentPage: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}