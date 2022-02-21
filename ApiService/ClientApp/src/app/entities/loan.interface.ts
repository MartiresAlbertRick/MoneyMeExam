export interface Loan {
    loanId ?: number;
    productId ?: number;
    customerId ?: number;
    loanAmount ?: number;
    repaymentTerms ?: number;
    interestAmount ?: number;
    establishmentFee ?: number;
    totalRepayments ?: number;
    repaymentFrequency ?: number;
    loanStatus ?: number;
    loanDetails ?: LoanDetail[];
}

export interface LoanDetail {
    loanDetailId ?: number;
    loanId ?: number;
    amount ?: number;
    dueDate ?: string;
}

export interface LoanDTO {
    loanAmount ?: number;
    repaymentTerms ?: number;
    firstName ?: string;
    lastName ?: string;
    title ?: string;
    dateOfBirth ?: string;
    mobile ?: string;
    email ?: string;
}
