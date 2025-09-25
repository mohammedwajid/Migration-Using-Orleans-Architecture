import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DynamicGrainRequest } from '../models/dynamic-grain-request.model';

@Injectable({ providedIn: 'root' })
export class SqsService {
  private api = 'http://localhost:6001/api/sqs';
  constructor(private http: HttpClient) {}
  send(request: any) { return this.http.post(`${this.api}/send`, request); }
}
