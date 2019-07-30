using System;
using System.ComponentModel;

namespace HBSQLite
{
    /// <summary>
    /// Provide the properties structure for column in the query string
    /// </summary>
    public class ColumnValue<T>
    {
        /// <summary>
        /// convert column to string 
        /// </summary>
        /// <param name="column">the column to convert</param>
        public static implicit operator string(ColumnValue<T> column) => column.ToString();

        /// <summary>
        /// convert column to the value type
        /// </summary>
        /// <param name="column">the column to convert</param>
        public static implicit operator T(ColumnValue<T> column) => column.Value;

        /// <summary>
        /// create new instance of <see cref="ColumnValue{T}" /> by <see cref="string" /></summary>
        /// <param name="value">the string value that should stored in object</param>
        /// <returns></returns>
        public static implicit operator ColumnValue<T>(T value)
        {
            return typeof(T) == typeof(string)
                ? new ColumnValue<T>(ColumnToValue.Like, value)
                : new ColumnValue<T>(value);
        }

        /// <summary>
        /// the Column for this Condition
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the way to search for this Column valve, the default value is Equal
        /// </summary>
        public ColumnToValue ColumnToValue { get; set; }

        /// <summary>
        /// the value for this column
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// initialize new instance of <see cref="ColumnValue{T}" /> that Fill all Columns
        /// </summary>
        /// <param name="name">the Column that will be used in the column</param>
        /// <param name="columnValueStates">the way to search for this Column valve</param>
        /// <param name="value">the value for this column</param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public ColumnValue(string name, ColumnToValue columnValueStates, T value)
        {
            if (!Enum.IsDefined(typeof(ColumnToValue), columnValueStates))
                throw new InvalidEnumArgumentException(nameof(columnValueStates), (int)columnValueStates,
                    typeof(ColumnToValue));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            if (value != null)
            {
                Name = name;
                ColumnToValue = columnValueStates;
                Value = value;
            }
            else
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// initialize new instance of <see cref="ColumnValue{T}" /> that Fill all Columns
        /// </summary>
        /// <param name="columnValueStates">the way to search for this Column valve</param>
        /// <param name="value">the value for this column</param>
        public ColumnValue(ColumnToValue columnValueStates, T value)
        {
            if (!Enum.IsDefined(typeof(ColumnToValue), columnValueStates))
                throw new InvalidEnumArgumentException(nameof(columnValueStates), (int)columnValueStates,
                    typeof(ColumnToValue));

            ColumnToValue = columnValueStates;
            Value = value;
        }

        /// <summary>
        /// initialize new instance of <see cref="ColumnValue{T}" /> with value
        /// </summary>
        /// <param name="value">the value </param>
        public ColumnValue(T value)
        {
            Value = value;
            ColumnToValue = ColumnToValue.Equal;
        }

        /// <summary>
        /// convert this column to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Value is string)
            {
                switch (ColumnToValue)
                {
                    case ColumnToValue.Equal:
                        return $"{Name} = '{Value}'";
                    case ColumnToValue.Like:
                        return $"{Name} like '%{Value}%'";
                    case ColumnToValue.NotLike:
                        return $"{Name} not like '%{Value}%'";
                    case ColumnToValue.GreaterThan:
                        return $"{Name} > '{Value}'";
                    case ColumnToValue.GreaterThanOrEqual:
                        return $"{Name} >= '{Value}'";
                    case ColumnToValue.SmallerThan:
                        return $"{Name} < '{Value}'";
                    case ColumnToValue.SmallerThanOrEqual:
                        return $"{Name} <= '{Value}'";
                    default:
                        throw new ArgumentNullException(nameof(ColumnToValue));
                }
            }

            switch (ColumnToValue)
            {
                case ColumnToValue.Equal:
                    return $"{Name} = {Value}";
                case ColumnToValue.Like:
                    return $"{Name} like {Value}";
                case ColumnToValue.NotLike:
                    return $"{Name} not like {Value}";
                case ColumnToValue.GreaterThan:
                    return $"{Name} > {Value}";
                case ColumnToValue.GreaterThanOrEqual:
                    return $"{Name} >= {Value}";
                case ColumnToValue.SmallerThan:
                    return $"{Name} < {Value}";
                case ColumnToValue.SmallerThanOrEqual:
                    return $"{Name} <= {Value}";
                default:
                    throw new ArgumentNullException(nameof(ColumnToValue));
            }
        }
    }
}
