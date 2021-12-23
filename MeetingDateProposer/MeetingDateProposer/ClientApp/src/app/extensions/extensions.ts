interface String {
    isEmpty(): boolean;
}

String.prototype.isEmpty = function (this: string): boolean {
    return (!this || this.length === 0 );
}