import { Injectable } from '@angular/core';
import { AstronautDutyRestService } from '../rest/astronaut-duty-rest-service';
import { map, Observable } from 'rxjs';
import { Getpersonbyname } from '../models/getpersonbyname';
import { ObjectResponse } from '../models/objectresponse';
import { CreateResponse } from '../models/createresponse';

@Injectable({
  providedIn: 'root'
})
export class AstronautDutyService {
  constructor(
    private astronautDutyRestService: AstronautDutyRestService
  ) { }

  getByName(name: string): Observable<Getpersonbyname> {
    return this.astronautDutyRestService.getByName(name).pipe(
          map((response: ObjectResponse<Getpersonbyname>) => response.data as Getpersonbyname)
        )
  }

  createDuty(name: string, rank:string, dutyTitle: string, dutyStartDate: Date): Observable<CreateResponse> {
    return this.astronautDutyRestService.createDuty(name, rank, dutyTitle, dutyStartDate).pipe(
      map((response: ObjectResponse<CreateResponse>) => response.data as CreateResponse)
    )
  }
}
