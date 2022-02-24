import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanApplicationDeniedComponent } from './loan-application-denied.component';

describe('LoanApplicationDeniedComponent', () => {
  let component: LoanApplicationDeniedComponent;
  let fixture: ComponentFixture<LoanApplicationDeniedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoanApplicationDeniedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanApplicationDeniedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
