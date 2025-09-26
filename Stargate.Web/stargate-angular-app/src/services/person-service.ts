import { Injectable } from '@angular/core';
import { PersonRestService } from '../rest/person-rest-service'
import { map, Observable } from 'rxjs';
import { GetPeopleResult } from '../models/getpeopleresult';
import { ObjectResponse } from '../models/objectresponse';
import { Getpersonbyname } from '../models/getpersonbyname';
import { CreateResponse } from '../models/createresponse';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  constructor(
    private personRestService: PersonRestService
  ) {}

  getPeople(): Observable<GetPeopleResult>{
    return this.personRestService.getPeople().pipe(
      map((response: ObjectResponse<GetPeopleResult>) => response.data as GetPeopleResult)
    );
  }

  getByName(name: string): Observable<Getpersonbyname>{
    return this.personRestService.getByName(name).pipe(
      map((response: ObjectResponse<Getpersonbyname>) => response.data as Getpersonbyname)
    )
  }

  create(name: string): Observable<CreateResponse>{
    return this.personRestService.create(name).pipe(
      map((response: ObjectResponse<CreateResponse>) => response.data as CreateResponse)
    )
  }
}
