import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AstronautDutyListComponent } from './astronaut-duty-list-component';

describe('AstronautDutyListComponent', () => {
  let component: AstronautDutyListComponent;
  let fixture: ComponentFixture<AstronautDutyListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AstronautDutyListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AstronautDutyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
