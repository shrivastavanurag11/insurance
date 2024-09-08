import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BeneficaryFormComponent } from './beneficary-form.component';

describe('BeneficaryFormComponent', () => {
  let component: BeneficaryFormComponent;
  let fixture: ComponentFixture<BeneficaryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BeneficaryFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BeneficaryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
