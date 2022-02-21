import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileNumbersComponent } from './mobile-numbers.component';

describe('MobileNumbersComponent', () => {
  let component: MobileNumbersComponent;
  let fixture: ComponentFixture<MobileNumbersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MobileNumbersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MobileNumbersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
