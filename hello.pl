#!/usr/bin/env perl
use Mojolicious::Lite -signatures;
use DateTime;

get '/' => sub ($c) {
  $c->render(template => 'index');
};

# 新しいルート: カレンダー表示
get '/calendar' => sub ($c) {
  my $year = $c->param('year') // DateTime->now->year;
  my $month = $c->param('month') // DateTime->now->month;

  # カレンダー生成ロジック（前回のサンプルを関数化）
  my $html = generate_calendar($year, $month);
  $c->render(text => $html, format => 'html');
};

sub generate_calendar {
  my ($year, $month) = @_;
  my $dt = DateTime->new(year => $year, month => $month, day => 1);
  my $today = DateTime->now;
  my $days_in_month = $dt->month_length;

  my $html = "<html><body><h1>$year 年 $month 月 カレンダー</h1><table border='1'>\n";
  my @weekdays = qw(日 月 火 水 木 金 土);
  $html .= "<tr><th>" . join("</th><th>", @weekdays) . "</th></tr>\n";

  my $current_day = 1;
  my $start_dow = $dt->day_of_week;
  $start_dow = 0 if $start_dow == 7;

  $html .= "<tr>";
  for (my $i = 0; $i < $start_dow; $i++) { $html .= "<td></td>"; }

  while ($current_day <= $days_in_month) {
    for (my $dow = 0; $dow < 7; $dow++) {
      if ($dow >= $start_dow && $current_day <= $days_in_month) {
        my $is_today = ($year == $today->year && $month == $today->month && $current_day == $today->day) ? "<b>$current_day</b>" : $current_day;
        $html .= "<td>$is_today</td>";
        $current_day++;
      } else {
        $html .= "<td></td>";
      }
    }
    $html .= "</tr>\n<tr>" if $current_day <= $days_in_month;
  }
  $html .= "</tr></table></body></html>\n";
  return $html;
}

app->start;
__DATA__

@@ index.html.ep
% layout 'default';
% title 'Welcome';
<h3>Welcome to the Mojolicious real-time web framework!!</h3>
<p><a class="btn btn-primary" href="/calendar">カレンダーを見る</a></p>
