import pygame
import sys
import random
import cv2
import numpy

def ball_animation():
    global ball_speed_x, ball_speed_y, player_score, opponent_score

    # Move animation
    ball.x += ball_speed_x
    ball.y += ball_speed_y

    # When ball out of window
    if ball.top <= 0 or ball.bottom >= screen_height:
        ball_speed_y *= -1

    if ball.left <= 0:
        player_score += 1
        tip_off()

    if ball.right >= screen_width:
        opponent_score += 1
        tip_off()

    # Condition for collide this player

    if ball.colliderect(player) or ball.colliderect(opponent):
        ball_speed_x *= -1


# When player go out of window
def player_animation():
    if player.top <= 0:
        player.top = 0
    if player.bottom >= screen_height:
        player.bottom = screen_height


def opponent_animation():
    # Opponent tracing ball
    if opponent.top < ball.y:
        opponent.top += opponent_speed_y
    if opponent.bottom > ball.y:
        opponent.bottom -= opponent_speed_y

    # When opponent go out of window
    if opponent.top <= 0:
        opponent.top = 0
    if opponent.bottom >= screen_height:
        opponent.bottom = screen_height

# When some one take a point, new one tip-off


def tip_off():
    global ball_speed_y, ball_speed_x

    ball.center = (screen_width / 2, screen_height / 2)
    ball_speed_y *= random.choice((1, -1))
    ball_speed_x *= random.choice((1, -1))

cap = cv2.VideoCapture(0)

cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)


def camera_capture():
    global point_x, point_y, is_trace

    _, frame = cap.read()

    hsv_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)

    low_green = numpy.array([40, 40, 40])
    high_green = numpy.array([70, 255, 255])

    mask_green = cv2.inRange(hsv_frame, low_green, high_green)

    moments = cv2.moments(mask_green, 1)

    dM01 = moments['m01']
    dM10 = moments['m10']
    dArea = moments['m00']

    if dArea > 150:
        point_x = int(dM10 / dArea)
        point_y = int(dM01 / dArea)
        cv2.circle(frame, (point_x, point_y), 10, (0, 0, 255), -1)
        is_trace = 1
    else:
        is_trace = 0

    cv2.imshow("Frame", frame)

# General setup
pygame.init()
clock = pygame.time.Clock()

# Main window
screen_width = 1280
screen_height = 720

# Speed of animation

ball_speed_x = 15 * random.choice((1, -1))
ball_speed_y = 15 * random.choice((1, -1))

player_speed_y = 0
opponent_speed_y = 17

# Point cord
point_y = 360

# Tracking status
is_trace = 0

# Text on screen
player_score = 0
opponent_score = 0
game_font = pygame.font.Font("freesansbold.ttf", 32)

screen = pygame.display.set_mode((screen_width, screen_height))
pygame.display.set_caption('PONG')

# game Rectangles
ball = pygame.Rect(screen_width / 2 - 10, screen_height / 2 - 10, 20, 20)
player = pygame.Rect(screen_width - 20, screen_height / 2 - 70, 10, 140)
opponent = pygame.Rect(10, screen_height / 2 - 70, 10, 140)

# colors
green_color = (0, 255, 0)
red_color = (255, 0, 0)
blue_color = (0, 0, 255)
black_color = pygame.Color('black')
white_color = pygame.Color('white')

while True:
    # Handling window

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            pygame.quit()
            sys.exit()

    camera_capture()

    if is_trace == 1:
        ball_animation()
        player_animation()
        opponent_animation()
        player.y = point_y

    # draw objects
    screen.fill(black_color)
    pygame.draw.aaline(screen, white_color, (screen_width / 2, 0), (screen_width / 2, screen_height))

    pygame.draw.rect(screen, green_color, player)
    pygame.draw.ellipse(screen, white_color, ball)
    pygame.draw.rect(screen, red_color, opponent)

    player_text = game_font.render(f"{player_score}", False, white_color)
    screen.blit(player_text, (660, 360))

    opponent_text = game_font.render(f"{opponent_score}", False, white_color)
    screen.blit(opponent_text, (600, 360))

    pygame.display.flip()
    clock.tick(60)
