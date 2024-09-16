import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class HttpHeaderService {
  
  private defaultHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  constructor() {}

  getHeaders(accessToken?: string): HttpHeaders {
    let headers = this.defaultHeaders;

    if (accessToken) {
      headers = headers.set('Authorization', `Bearer ${accessToken}`);
    }

    return headers;
  }

  //add getcustom header if needed additional options(security keys or other api metadata)
}
