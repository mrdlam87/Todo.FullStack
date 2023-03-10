import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-list-card',
  templateUrl: './list-card.component.html',
  styleUrls: ['./list-card.component.scss'],
})
export class ListCardComponent {
  @Input() title: string;
  @Output() onAddEvent = new EventEmitter<any>();

  onAddClick() {
    this.onAddEvent.emit();
  }
}
