export interface MatterInvoice {
  id: number;
  matterId: number;
  matterName: string;
  clientName: string;
  attorneyId: number;
  attorneyName: string;
  ratePerHour: number;
  timeSpent: number;
  totalAmount: number;
  date: Date;
}