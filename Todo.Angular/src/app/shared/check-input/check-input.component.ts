import { Component, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-check-input',
  templateUrl: './check-input.component.html',
  styleUrls: ['./check-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: CheckInputComponent,
    },
  ],
})
export class CheckInputComponent implements ControlValueAccessor {
  @Input() label: string;
  checked: boolean;
  onChange = (checked) => {};
  onTouched = () => {};
  touched = false;
  disabled = false;

  onClick() {
    this.markAsTouched();
    this.checked = !this.checked;
    this.onChange(this.checked);
  }

  writeValue(checked: boolean): void {
    this.checked = checked;
  }

  registerOnChange(onChange: any): void {
    this.onChange = onChange;
  }

  registerOnTouched(onTouched: any): void {
    this.onTouched = onTouched;
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }
}
