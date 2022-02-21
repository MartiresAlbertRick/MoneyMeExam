import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileNumberDetailsComponent } from './mobile-number-details.component';

describe('MobileNumberDetailsComponent', () => {
  let component: MobileNumberDetailsComponent;
  let fixture: ComponentFixture<MobileNumberDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MobileNumberDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MobileNumberDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
