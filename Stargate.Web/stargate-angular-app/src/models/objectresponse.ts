export interface ObjectResponse<T> {
  success: boolean;
  message?: string;
  data?: T | null;
}
