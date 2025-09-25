import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeSearchComponent } from './employee-search';

describe('EmployeeSearch', () => {
  let component: EmployeeSearchComponent;
  let fixture: ComponentFixture<EmployeeSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeSearchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
