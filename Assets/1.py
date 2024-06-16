def solve():
    import sys
    input = sys.stdin.read
    data = input().split()

    n = int(data[0])
    index = 1
    vertical_count = 0
    horizontal_count = 0

    for _ in range(n):
        a = int(data[index])
        b = int(data[index + 1])
        c = int(data[index + 2])
        index += 3

        if a != 0:
            vertical_count += 1
        elif b != 0:
            horizontal_count += 1

    if vertical_count >= 2 or horizontal_count >= 2:
        print("VOID")
    elif vertical_count >= 1 and horizontal_count >= 1:
        print("BOUNDED")
    else:
        print("UNBOUNDED")
