import csv
import matplotlib.pyplot as plt


def load_burndown(path):
    days, ideal, actual, notes = [], [], [], []
    with open(path, newline="") as f:
        for row in csv.DictReader(f):
            days.append(int(row["day"]))
            ideal.append(float(row["ideal_remaining_points"]))
            actual.append(float(row["actual_remaining_points"]))
            notes.append(row["note"])
    return days, ideal, actual, notes


def load_velocity(path):
    sprints, committed, completed = [], [], []
    with open(path, newline="") as f:
        for row in csv.DictReader(f):
            sprints.append(row["sprint"])
            committed.append(float(row["committed_points"]))
            completed.append(float(row["completed_points"]))
    return sprints, committed, completed


def plot_burndown():
    days, ideal, actual, notes = load_burndown("burndown-data.csv")

    fig, ax = plt.subplots(figsize=(9, 5.5))
    ax.plot(days, ideal, linestyle="--", color="#9aa0a6", label="Ideal burndown")
    ax.plot(days, actual, marker="o", color="#1a73e8", label="Actual burndown")

    blocked_index = notes.index("Blocked - flagged in Daily Scrum")
    ax.annotate(
        "Blocker flagged\nin Daily Scrum",
        xy=(days[blocked_index], actual[blocked_index]),
        xytext=(days[blocked_index] - 2.6, actual[blocked_index] + 2.2),
        arrowprops=dict(arrowstyle="->", color="#b00020"),
        color="#b00020",
        fontsize=9,
    )

    ax.set_title("Sprint 3 Burndown Chart")
    ax.set_xlabel("Sprint Day")
    ax.set_ylabel("Remaining Story Points")
    ax.set_xticks(days)
    ax.set_ylim(bottom=0)
    ax.legend()
    ax.grid(True, alpha=0.3)

    fig.tight_layout()
    fig.savefig("sprint-burndown-chart.png", dpi=150)
    plt.close(fig)


def plot_velocity():
    sprints, committed, completed = load_velocity("velocity-data.csv")
    x = range(len(sprints))
    width = 0.35

    fig, ax = plt.subplots(figsize=(8, 5))
    ax.bar([i - width / 2 for i in x], committed, width, label="Committed", color="#9aa0a6")
    ax.bar([i + width / 2 for i in x], completed, width, label="Completed", color="#1a73e8")

    average_velocity = sum(completed[:2]) / 2
    ax.axhline(average_velocity, color="#b00020", linestyle="--", linewidth=1)
    ax.text(
        len(sprints) - 1,
        average_velocity + 0.3,
        f"Avg velocity after Sprint 1-2: {average_velocity:.0f} pts",
        color="#b00020",
        fontsize=8,
        ha="right",
    )

    ax.set_title("Team Velocity by Sprint")
    ax.set_xlabel("Sprint")
    ax.set_ylabel("Story Points")
    ax.set_xticks(list(x))
    ax.set_xticklabels(sprints)
    ax.legend()
    ax.grid(True, axis="y", alpha=0.3)

    fig.tight_layout()
    fig.savefig("velocity-chart.png", dpi=150)
    plt.close(fig)


if __name__ == "__main__":
    plot_burndown()
    plot_velocity()
    print("Generated sprint-burndown-chart.png and velocity-chart.png")
