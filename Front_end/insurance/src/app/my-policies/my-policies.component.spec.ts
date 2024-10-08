import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyPoliciesComponent } from './my-policies.component';

describe('MyPoliciesComponent', () => {
  let component: MyPoliciesComponent;
  let fixture: ComponentFixture<MyPoliciesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyPoliciesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyPoliciesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
