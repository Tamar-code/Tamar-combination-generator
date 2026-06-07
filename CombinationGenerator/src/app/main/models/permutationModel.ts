export interface StartResponse {
  totalCount: string;
  sessionId: string;
}

export interface NextResponse {
  index: string;
  permutation: number[];
  hasMore: boolean;
}

export interface PermutationItem {
  index: string;
  permutation: number[];
}

export interface GetAllResponse {
  permutations: PermutationItem[];
  hasMore: boolean;
  totalPages: number;
}
