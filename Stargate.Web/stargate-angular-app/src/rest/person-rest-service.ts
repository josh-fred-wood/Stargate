import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetPeopleResult } from '../models/getpeopleresult';
import { Getpersonbyname } from '../models/getpersonbyname';

@Injectable({
  providedIn: 'root'
})
export class PersonRestService {
  constructor(private http: HttpClient) { }

  getPeople(): Observable<GetPeopleResult> {
    return this.http.get<GetPeopleResult>(`http://localhost:8080/person/getpeople`)
  }

  getByName(name: string): Observable<Getpersonbyname> {
    return this.http.get<Getpersonbyname>(`http://localhost:8080/person/getpeople/${name}`)
  }

  create(name: string): Observable<Response> {
    const payload = {
      name: name
    }
    return this.http.post<Response>(`http://localhost:8080/person`, payload);
  }
}
