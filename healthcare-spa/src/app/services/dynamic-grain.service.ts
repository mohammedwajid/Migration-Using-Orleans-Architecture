import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface DynamicGrainRequest {
  grainType: string;
  grainKey: string;
  parameters?: { [key: string]: any };
}

@Injectable({
  providedIn: 'root'
})
export class DynamicGrainService {
  private apiUrl = 'https://localhost:5001/api/DynamicGrain/invoke'; // Update port if needed

  constructor(private http: HttpClient) { }

  invokeGrain(request: DynamicGrainRequest): Observable<any> {
    return this.http.post<any>(this.apiUrl, request);
  }
}
