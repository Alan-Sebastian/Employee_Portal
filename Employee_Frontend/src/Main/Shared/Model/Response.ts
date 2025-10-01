export interface ResponseData<T = any> {
  statusCode: number;
  message: string;
  data: T;
  jwtToken: string;
}

